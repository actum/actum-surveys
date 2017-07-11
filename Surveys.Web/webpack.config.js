/// <binding ProjectOpened='Watch - Development' />
"use strict";

var WebpackNotifierPlugin = require('webpack-notifier');

module.exports = {
    entry: [
        __dirname + "/src/Js/jquery-2.0.3.js",
        __dirname + "/src/Js/bootstrap.js",
        __dirname + "/src/Css/bootstrap.less",
        __dirname + "/src/Css/page.less",
        __dirname + "/src/Css/theme.less"
    ],
    output: {
        path: __dirname + '/wwwroot/dist/',
        publicPath: "/dist/",
        filename: "bundle.js"
    },
    devServer: {
        contentBase: ".",
        host: "localhost",
        port: 9000
    },
    module: {
        loaders: [
            {
                test: /\.jsx?$/,
                loader: "babel-loader"
            },
            {
                test: /\.css$/,
                loader: "style-loader!css-loader"
            },
            {
                test: /\.less$/,
                loader: "style-loader!css-loader!less-loader"
            },
            {
                test: /\.(jpe?g|png|gif|svg)$/i,
                loaders: [
                    'file-loader?hash=sha512&digest=hex&name=[hash].[ext]',
                    //'image-webpack-loader?bypassOnDebug&optimizationLevel=7&interlaced=false'
                ]
            },
            {
                test: /\.(eot|svg|ttf|woff|woff2)($|\?)/, 
                loader: 'file-loader?hash=sha512&digest=hex&name=[name].[ext]'
            }
        ]
    },
    plugins: [
        new WebpackNotifierPlugin({ /* title: 'Webpack' */ }),
    ]
};