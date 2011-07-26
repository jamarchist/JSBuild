(function ($) {

    $.copy({
        sources: ['Default.js'],
        destinations: ['C:\\Temp\\Default.js']
    });

    $.call('CompileSolution.js');
    $.call('ExecuteSqlScripts.js');

    print($.pathOf('CompileSolution.js'));

    print('execution complete');

    $.call('example.coffee');

    //print(CoffeeScript.compile('a = (x) -> x * x', { bare: true }));

})(JSBuild);