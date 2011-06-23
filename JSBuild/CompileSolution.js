(function ($) {
    $.include('SolutionProperties.js');

    var sln = solutionProperties;

    $.execute.msbuild({
        projectFile: sln.SolutionFile,
        properties: {
            Configuration: sln.Configuration,
            Platform: sln.Platform
        }
    });

})(JSBuild);