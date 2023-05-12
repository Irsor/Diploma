class SaveDataSourceExtension {
    toolbox;
    menuItem;
    dashboardControl;
    name = "dashboard-save-data-source";
    newName = ko.observable("dataSource2.json");

    constructor(dashboardControl) {
        this.dashboardControl = dashboardControl;
        this.menuItem = {
            id: "dashboard-save-data-source",
            title: "New Data Source",
            template: "dx-save-data-source-form",
            selected: ko.observable(true),
            disabled: ko.computed(function () { return !dashboardControl.dashboard(); }),
            index: 112,
            data: this
        };
    }

    saveAs() {
        if (this.isExtensionAvailable()) {
            this.toolbox.menuVisible(false);
            $.ajax({
                url: 'FileLoader/File',
                data: { DataSourceFileName: this.newName },
                type: 'POST'
            });
        }
    }

    isExtensionAvailable() {
        return this.toolbox !== undefined && this.newDashboardExtension !== undefined;
    }

    start() {
        this.toolbox = this.dashboardControl.findExtension("toolbox");
        this.newDashboardExtension = this.dashboardControl.findExtension("create-dashboard");

        if (this.isExtensionAvailable()) {
            this.toolbox.menuItems.push(this.menuItem);
        }
    }
    stop() {
        if (this.isExtensionAvailable()) {
            this.toolbox.menuItems.remove(this.menuItem);
        }            
    }
}