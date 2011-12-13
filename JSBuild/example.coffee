do ->
	$ = JSBuild

	copyArgs =
		sources: ['Default.js']
		destinations: ['C:\\Temp\\Default.js']

	$.copy copyArgs

	$.call 'CompileSolution.js'
	$.call 'ExecuteSqlScripts.js'

	print $.pathOf 'CompileSolution.js'

	print 'execution complete'