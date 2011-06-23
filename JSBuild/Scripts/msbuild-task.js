(function ($) {
    $.execute = $.execute || {};

    $.execute.msbuild = function (options) {
        var propertiesBuilder = function (qualifier, properties) {
            var buildProperties = '';
            if (properties && properties.length > 0) {
                buildProperties = qualifier;

                for (propertyName in properties) {
                    buildProperties = buildProperties + '{0}={1};'.format(propertyName, properties[propertyName]);
                }
            }

            return buildProperties;
        };

        var projectFile = options.projectFile || '';
        var configuration = propertiesBuilder('/p:', options.properties);
        var targets = propertiesBuilder('/t:', options.targets);

        var formattedArgs = '{0} {1} {2}'.format(projectFile, configuration, targets);

        $.exec({
            Path: "C:\\Windows\\Microsoft.NET\\Framework\\v4.0.30319\\MSBuild.exe",
            Args: formattedArgs
        });
    };

})(JSBuild);

