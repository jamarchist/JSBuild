var JSBuild = {};
JSBuild.Task = {};
JSBuild.Task.Log = {};

JSBuild.Task.Log.writeInfo = function (message) {
    print(message);
};

JSBuild.Task.Log.writeError = function (message) {
    print('*** Error ***');
    print(message);
    print('*** End Error***');
};