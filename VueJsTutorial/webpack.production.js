/// <binding ProjectOpened='Watch - Development, Watch - Production' />

"use strict";

const path = require('path');

const webpack = require('webpack');

module.exports = {
    
    entry: {
        app: './wwwroot/js/site.js',
        HelloWorld: './Views/Home/HelloWorld.cshtml.js',
        EmployeeIndex: './Views/Home/EmployeeIndex.cshtml.js',
        TestView: './Views/Home/TestView.cshtml.js'
     
    },
    plugins: [
        new webpack.ProvidePlugin({
            '$': 'jquery',
            jQuery: 'jquery',
            'window.jQuery': 'jquery',
            Popper: ['popper.js', 'default'],
            moment: 'moment',
            axios: 'axios',
            numbro: 'numbro',
            highChart : 'highchart'
        }),
        new webpack.optimize.UglifyJsPlugin()
    ],
    output: {
        publicPath: "/js/",
        path: path.join(__dirname, '/wwwroot/js/'),
        filename: '[name].bundle.js'
    },
    devtool: false ,
    module: {
        rules: [
            {
                test: /\.js$/,
                loader: 'babel-loader',
                exclude: /(node_modules)/,
                query: {
                    presets: ['es2015']
                }
            },
            {
                test: /\.ts$/,
                exclude: /node_modules|vue\/src/,
                loader: "ts-loader",
                options: {
                    appendTsSuffixTo: [/\.vue$/]
                }
            },
            {
                test: /\.css$/,
                loaders: ['style-loader', 'css-loader']
            },
            {
                test: /\.(png|jpg|gif)$/,
                use: {
                    loader: 'url-loader',
                    options: {
                        limit: 8192
                    }
                }
            },
            {
                test: /\.vue$/,
                loader: 'vue-loader',
            }
        ]
    },
    resolve: {
        alias: {
            vue: 'vue/dist/vue.js'
        },
        extensions: ['.js', '.vue']
    }
};