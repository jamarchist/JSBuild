include('Sql.js');
include('MSBuild.js');

(function ($, $$) {
    var scripts = ["TestQuery.sql", "TestQuery2.sql"];
    $$.sql(scripts);

    $$.msbuild();

    print('execution complete');

})(JSBuild, CustomTasks);