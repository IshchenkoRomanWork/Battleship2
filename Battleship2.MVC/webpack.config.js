const path = require('path');

module.exports = {
	entry:
	{
		testReactSignalR: './Battleship2.MVC/wwwroot/js/dist/testReactSignalR.jsx',
		reactgamehub: './Battleship2.MVC/wwwroot/js/dist/reactgamehub.jsx'
	},
	output: {
		filename: '[name].js',
		globalObject: 'this',
		path: path.resolve(__dirname, 'wwwroot/js'),
		publicPath: 'js/'
	},
	mode: process.env.NODE_ENV === 'production' ? 'production' : 'development',
	module: {
		rules: [
			{
				test: /\.jsx?$/,
				exclude: /node_modules/,
				loader: 'babel-loader',
			},
		],
	}
};