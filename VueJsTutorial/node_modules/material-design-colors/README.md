# [Google Material Design color palette](https://github.com/ValentinGot/material-design-colors)

[![npm version](https://badge.fury.io/js/material-design-colors.svg)](https://badge.fury.io/js/material-design-colors)
[![Bower version](https://badge.fury.io/bo/material-design-colors.svg)](https://badge.fury.io/bo/material-design-colors)

> The [Google Material Design](https://www.google.com/design/spec/style/color.html) color palette for Sass and Less

Provides quick color palettes for [Sass](http://sass-lang.com/) and [Less](http://lesscss.org/) projects.

## Install

You may install this module via bower or npm.

```
$ bower install material-design-colors
$ npm install material-design-colors
```

## Quick use

### Sass

To use the Sass variables, you can import the **colors** file like this:

``` sass
@import 'material-design-colors/colors'

.btn {
    color: $white-color;
    background-color: $red-500-color;
}
```

### Less

To use the Less variables, you can import the **colors** file like this:

``` less
@import 'material-design-colors/colors'

.btn {
    color: @white-color;
    background-color: @red-500-color;
}
```

### Classes

To use the CSS classes variables, you can import the **dist/material-design.min.css** CSS in your html.

There is two different set of CSS classes :
* The ones made to change the **color** attribute (.{color}-{accent}-cl)
* The ones made to change the **background-color** attribute (.{color}-{accent}-bg)

``` html
<html>
    <head>
        <link rel="stylesheet" href="material-design-colors/dist/material-design.min.css" />
    </head>
    
    <body>
        <button class="white-cl red-500-bg">Material design colors</button>
    </body>
</html>
```

## License

This module is released under the MIT license.

https://github.com/ValentinGot/material-design-colors/blob/master/LICENSE