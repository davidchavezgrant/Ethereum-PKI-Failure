const path = require("path");
const webpack = require('webpack');

module.exports = {
    plugins: [
        // Work around for Buffer is undefined:
        // https://github.com/webpack/changelog-v5/issues/10
        new webpack.ProvidePlugin({
            Buffer: ['buffer', 'Buffer'],
        }),
        new webpack.ProvidePlugin({
            process: 'process/browser',
        }),
    ],
    module: {
        rules: [
            {
                test: /\.(js|jsx)$/,
                exclude: /node_modules/,
                use: {
                    loader: "babel-loader"
                }
            }
        ]
    },
    resolve: {
        extensions: [ '.ts', '.js' ],
        fallback: {
            "stream": require.resolve("stream-browserify"),
            "buffer": require.resolve("buffer")
        }
    },
    output: {
        path: path.resolve(__dirname, '../wwwroot/js'),
        filename: "index.js",
        library: "JsLib"
    }
};