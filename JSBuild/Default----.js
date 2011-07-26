//
// An example build file
//
(function ($) {

    $.include('Properties.js');

    var copier = function () {

        $.copy({
            SourceFiles: ["Default.js"],
            DestinationFiles: ["C:\\Temp\\Default.js"]
        });

    };

    var deleter = function () {

        $.deleteFiles({
            Paths: ["C:\\Temp\\Default.js"],
            NumberOfRetries: 10
        });

    };

    for (i = 0; i < 5; i++) {

        print('function variables defined.');
        print('copying....');
        copier();

        print('deleting....');
        deleter();
    }

    var exitCode = $.exec({
        Path: "sc.exe",
        Args: "/?"
    });

    properties.ExitCode = exitCode.toString();

    // ----- //

    print('');

    print('Build Profile: ' + properties.Profile);
    print('Build Client: ' + properties.Client);
    print('Exit Code from sc: ' + properties.ExitCode);

})(JSBuild);


