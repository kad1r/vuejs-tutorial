/// <binding ProjectOpened='Watch - Development, Watch - Production' />

"use strict";

const path = require('path');
const webpack = require('webpack');
const glob = require('glob');

function getEntries() {
    return glob.sync('./Views/**/**.js')
        .map((file) => {
            var splitedFilePath = file.split("/");
            var fileFolder = splitedFilePath[splitedFilePath.length - 3];
            console.log(fileFolder);
            return {
                name: fileFolder + "__" + path.parse(file).name,
                path: file
            }
        }).reduce((memo, file) => {
            memo[file.name] = file.path
            return memo;
        }, {})
}

module.exports = {
    
    entry: getEntries(),
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
    ],
    output: {
        publicPath: "/js/",
        path: path.join(__dirname, '/wwwroot/js/'),
        chunkFilename: (arg) => {
               return arg.chunk.name +".js";
        },
        filename: (arg) => {
            var filePath = arg.chunk.entryModule.rawRequest;
            var fileName = arg.chunk.name.split("__")[1];
            var fileFolder = path.parse(filePath).dir.replace("/Js", "");
            return fileFolder + '/' + fileName + '.bundle.js';
        }
    },
    devtool: 'eval-source-map' ,
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