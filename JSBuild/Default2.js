(function ($) {

    include('StringExtensions.js');
    include('Properties.js');

    //    var args = "-S " + properties.DatabaseServer +
    //                " -U " + properties.DatabaseUserName +
    //                " -P " + properties.DatabasePassword +
    //                " -i " + "TestQuery.sql";



    var args = '-S {0} -U {1} -P {2} -e -i {3}'.format(
        properties.DatabaseServer, properties.DatabaseUserName, properties.DatabasePassword, 'TestQuery.sql');

    //for (i = 0; i < 2; i++) {

        $.exec({
            Path: "sqlcmd",
            Args: args
        });

    //}



    print('execution complete');

})(JSBuild);