/// <reference path="typings/lib.d.ts" />

module Higgins {
    export class Web {
        constructor() {

        }
        start() {
            $(document).ready(() => {
                console.log("Good evening sir.");
            });
        }
    }
}

this.app = new Higgins.Web();
this.app.start();