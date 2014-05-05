using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace Higgins.Core
{
    public class Git
    {
        public static async Task<GitRevParseResult> RevParse(string wd, IEnumerable<string> args = null)
        {
            var argsL = new List<string>
            {
                "rev-parse",
            };

            if (args != null)
            {
                argsL.AddRange(args);
            }

            var res = await Execute(wd, argsL).ConfigureAwait(false);

            return new GitRevParseResult(res.Code, res.Output);
        }
        public static async Task<GitLogResult> Log(string wd, IEnumerable<string> args = null)
        {
            var argsL = new List<string>
            {
                "log",
                "--format=%H|%at|%an|%ae|%s"
            };

            if (args != null)
            {
                argsL.AddRange(args);
            }

            var res = await Execute(wd, argsL).ConfigureAwait(false);

            return new GitLogResult(res.Code, res.Output);
        }

        public static async Task<GitRawResult> Execute(string wd, IEnumerable<string> args)
        {
            var sInfo = InitGitProcess(wd, args);

            using (var p = new Process
            {
                StartInfo = sInfo,
                EnableRaisingEvents = true,
            })
            {
                p.Start();

                p.BeginErrorReadLine();
                p.BeginOutputReadLine();

                var sbStdOut = new StringBuilder();
                var sbStdErr = new StringBuilder();
                var sbOut    = new StringBuilder();

                p.OutputDataReceived += (sender, eventArgs) =>
                {
                    sbStdOut.AppendLine(eventArgs.Data);
                    sbOut.AppendLine(eventArgs.Data);
                };

                p.ErrorDataReceived += (sender, eventArgs) =>
                {
                    sbStdErr.AppendLine(eventArgs.Data);
                    sbOut.AppendLine(eventArgs.Data);
                };

                // ReSharper disable once AccessToDisposedClosure
                await Task.Run(() => p.WaitForExit()).ConfigureAwait(false);

                p.CancelErrorRead();
                p.CancelOutputRead();

                return new GitRawResult
                {
                    Code           = p.ExitCode,
                    Output         = sbOut.ToString().Trim(),
                    StandardError  = sbStdOut.ToString().Trim(),
                    StandardOutput = sbStdErr.ToString().Trim()
                };
            }
        }
        private static ProcessStartInfo InitGitProcess(string wd, IEnumerable<string> args)
        {
            return new ProcessStartInfo
            {
                CreateNoWindow         = true,
                RedirectStandardError  = true,
                RedirectStandardOutput = true,
                UseShellExecute        = false,
                WorkingDirectory       = wd,
                StandardOutputEncoding = Encoding.UTF8,
                StandardErrorEncoding  = Encoding.UTF8,
                FileName               = "git.exe",
                Arguments              = string.Join(" ", args)
            };
        }
    }
}
