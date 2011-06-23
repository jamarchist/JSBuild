

(function ($) {

    $.include('Scripts\\StringExtensions.js');
    $.include('Scripts\\Sql.js');
    $.include('Scripts\\MSBuild.js');

    $.copy({
        sources: ['Default.js'],
        destinations: ['C:\\Temp\\Default.js']
    });

    $.call('CompileSolution.js');
    $.call('ExecuteSqlScripts.js');

    print('execution complete');

})(JSBuild);