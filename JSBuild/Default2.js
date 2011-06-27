(function ($) {

    $.copy({
        sources: ['Default.js'],
        destinations: ['C:\\Temp\\Default.js']
    });

    $.call('CompileSolution.js');
    $.call('ExecuteSqlScripts.js');

    print($.pathOf('CompileSolution.js'));

    print('execution complete');

})(JSBuild);