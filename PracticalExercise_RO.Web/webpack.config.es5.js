'use strict';

var path = require('path');
var webpack = require('webpack');
var VueLoaderPlugin = require('vue-loader/lib/plugin');
var MiniCssExtractPlugin = require("mini-css-extract-plugin");
var OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');
var UglifyJsPlugin = require('uglifyjs-webpack-plugin');

var bundleOutputDir = path.resolve(__dirname, './wwwroot/');
var sourcePath = path.resolve(__dirname, './VueComponents/');
var nodemodulesPath = path.resolve(__dirname, './node_modules/');

//----------------------
//Check about env variables at https://webpack.js.org/guides/environment-variables/
//---------------------------
module.exports = function (env) {
    var isDevBuild = env.NODE_ENV === 'development';
    console.log(isDevBuild ? 'This is DEV build' : 'This is PROD build');

    return {
        entry: ['./VueComponents/registerVue.js', './SCSS/site.scss'],
        output: {
            path: bundleOutputDir,
            filename: 'js/site.bundle.js'
        },
        optimization: {
            minimizer: [new UglifyJsPlugin({
                cache: true,
                parallel: true,
                sourceMap: true // set to true if you want JS source maps
            }), new OptimizeCssAssetsPlugin({})]
        },
        plugins: [new webpack.DefinePlugin({ 'process.env': { NODE_ENV: isDevBuild ? '"development"' : '"production"' } }), new VueLoaderPlugin(), new UglifyJsPlugin({
            test: /\.js(\?.*)?$/i,
            uglifyOptions: {
                warnings: false,
                parse: {},
                compress: {},
                mangle: true, // Note `mangle.properties` is `false` by default.
                output: null,
                toplevel: false,
                nameCache: null,
                ie8: false,
                keep_fnames: false
            },
            extractComments: false
        }), new MiniCssExtractPlugin({
            filename: "./css/site.bundle.css",
            publicPath: "./wwwroot/css"
        }), new OptimizeCssAssetsPlugin({
            assetNameRegExp: /\.css$/,
            cssProcessor: require('cssnano'),
            cssProcessorPluginOptions: {
                preset: ['default', { discardComments: { removeAll: true } }]
            },
            canPrint: true
        })],
        module: {
            rules: [{ test: /\.vue$/, loader: 'vue-loader', include: sourcePath }, { test: /\.js$/, loader: 'babel-loader', include: sourcePath, exclude: nodemodulesPath, options: { presets: ['env'] } }, { test: /\.scss$/,
                use: [{ loader: MiniCssExtractPlugin.loader }, { loader: "css-loader" }, { loader: "sass-loader" }]
            }]
        },
        resolve: {
            alias: {
                'vue$': path.resolve(__dirname, 'node_modules/vue/dist/vue.common.js')
            },
            //vue$: path.resolve(__dirname, './node_modules/vue/dist/vue.esm.js'),
            extensions: ['*', '.js', '.vue', '.json']
        },
        performance: {
            hints: false
        },
        devtool: isDevBuild ? '#eval-source-map' : ''
    };
};

