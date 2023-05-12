class RedirectToMenuExtension {
    toolbox;
    menuItem;
    dashboardControl;
    name = "return-to-menu";

    constructor(dashboardControl) {
        this.dashboardControl = dashboardControl;
        this.menuItem = {
            id: this.name,
            title: "Return to Menu",
            click: this.returnToMenu.bind(this),
            selected: ko.observable(false),
            disabled: ko.computed(function () { return !this.dashboardControl.dashboard(); }, this),
            index: 113,
            hasSeparator: true,
            data: this
        };
    }

    returnToMenu() {
        $.ajax({
            url: 'FileLoader/ReturnToMenu',
            data: { Result: 'result' },
            type: 'POST'
        });
    }
    

    start() {
        this.toolbox = this.dashboardControl.findExtension("toolbox");
        this.toolbox && this.toolbox.menuItems.push(this.menuItem);
    }

    stop() {
        this.toolbox && this.toolbox.menuItems.remove(this.menuItem);
    }
}