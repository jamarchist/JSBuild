(function ($) {
    include('DatabaseProperties.js');
    include('StringExtensions.js');

    if (!$.execute) {
        $.execute = {};
    }

    // The 'sql' task. Executes an array of sql scripts
    $.execute.sql = function (files) {
        var db = databaseProperties;

        for (fileIndex in files) {
            //var formattedArgs = '-S {0} -d {1} -U {2} -P {3} -e -i {4}'.format(
            //    db.DatabaseServer, db.DatabaseName, db.DatabaseUserName, db.DatabasePassword, files[fileIndex]);

            var formattedArgs = '-S {0} -d {1} -i {2}'.format(
                db.DatabaseServer, db.DatabaseName, files[fileIndex]);

            $.exec({
                Path: "sqlcmd",
                Args: formattedArgs
            });
        }
    };

    $.execute.sql = function (options) {
        for (fileIndex in options.files) {
            var formattedArgs;

            if (!options.userName) {
                formattedArgs = '-S {0} -d {1} -i {2}'.format(
                options.server, options.database, options.files[fileIndex]);
            }
            else {
                formattedArgs = '-S {0} -d {1} -U {2} -P {3} -i {4}'.format(
                    options.server, options.database, options.userName, db.password, options.files[fileIndex]);
            }

            $.exec({ Path: "sqlcmd", Args: formattedArgs });
        }

    };

})(JSBuild);