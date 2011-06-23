(function ($) {
    $.execute = $.execute || {};

    $.execute.sql = function (options) {
        for (fileIndex in options.files) {
            var formattedArgs;

            if (!options.userName) {
                formattedArgs = '-S {0} -d {1} -i {2}'.format(
                    options.server, options.database, options.files[fileIndex]);
            }
            else {
                formattedArgs = '-S {0} -d {1} -U {2} -P {3} -i {4}'.format(
                    options.server, options.database, options.userName, options.password, options.files[fileIndex]);
            }

            $.exec({ Path: "sqlcmd", Args: formattedArgs });
        }

    };

})(JSBuild);