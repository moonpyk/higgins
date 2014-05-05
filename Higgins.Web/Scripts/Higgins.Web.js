/// <reference path="typings/lib.d.ts" />
var Higgins;
(function (Higgins) {
    var Web = (function () {
        function Web() {
        }
        Web.prototype.start = function () {
            $(document).ready(function () {
                console.log("Good evening sir.");
            });
        };
        return Web;
    })();
    Higgins.Web = Web;
})(Higgins || (Higgins = {}));

this.app = new Higgins.Web();
this.app.start();
//# sourceMappingURL=Higgins.Web.js.map
