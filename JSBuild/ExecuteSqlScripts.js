(function ($) {
    $.include('DatabaseProperties.js');

    var db = databaseProperties;
    var scripts = $.getFiles({
        directory: 'C:\\Projects\\github\\JSBuild-Clr4-64\\JSBuild\\bin\\x64\\Debug',
        pattern: '*.sql'
    });

    print(scripts);

    $.execute.sql({
        server: db.DatabaseServer,
        database: db.DatabaseName,
        userName: db.DatabaseUserName,
        password: db.DatabasePassword,
        files: scripts
    });
})(JSBuild);

