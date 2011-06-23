include('Sql.js');
include('MSBuild.js');

(function ($) {
    var scripts = ["TestQuery.sql", "TestQuery2.sql"];
    $.execute.sql(scripts);

    $.execute.msbuild();

    print('execution complete');

})(JSBuild);