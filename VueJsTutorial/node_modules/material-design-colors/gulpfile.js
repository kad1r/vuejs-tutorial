'use strict';

var gulp = require('gulp'),
    rename = require('gulp-rename'),
    sass = require('gulp-ruby-sass'),
    minifycss = require('gulp-minify-css'),
    sourcemaps = require('gulp-sourcemaps'),
    del = require('del');

// Compute CSS
gulp.task('styles', function() {
    return sass('material-design.scss', { style: 'expanded' })
        .pipe(sourcemaps.init())
        .pipe(sourcemaps.write('./maps'))
        .pipe(gulp.dest('dist'))
        .pipe(rename({suffix: '.min'}))
        .pipe(minifycss())
        .pipe(sourcemaps.init())
        .pipe(gulp.dest('dist'));
});

// Watch
gulp.task('watch', function() {
    gulp.watch('*.scss', ['styles']);
});

// Clean
gulp.task('clean', function() {
    return del(['dist']);
});

// Main
gulp.task('default', ['clean'], function() {
    gulp.start('styles', 'watch');
});