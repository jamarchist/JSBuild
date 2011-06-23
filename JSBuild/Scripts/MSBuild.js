//var CustomTasks = CustomTasks || {};

(function ($) {
    if (!$.execute) {
        $.execute = {};
    }

    include('SolutionProperties.js');
    include('StringExtensions.js');

    var sln = solutionProperties;

    // The 'msbuild' task. Runs MSBuild against a solution
    $.execute.msbuild = function (options) {
        var formattedArgs = '{0} /p:Configuration={1};Platform={2}'.format(sln.SolutionFile, sln.Configuration, sln.Platform);

        $.exec({
            Path: "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe",
            Args: formattedArgs
        });

    };

})(JSBuild);

