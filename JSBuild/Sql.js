var CustomTasks = CustomTasks || {};

(function ($) {
    include('DatabaseProperties.js');
    include('StringExtensions.js');

    // The 'sql' task. Executes an array of sql scripts
    CustomTasks.sql = function (files) {
        var db = databaseProperties;

        for (fileIndex in files) {
            var formattedArgs = '-S {0} -d {1} -U {2} -P {3} -e -i {4}'.format(
                db.DatabaseServer, db.DatabaseName, db.DatabaseUserName, db.DatabasePassword, files[fileIndex]);

            $.exec({
                Path: "sqlcmd",
                Args: formattedArgs
            });
        }
    };

})(JSBuild);