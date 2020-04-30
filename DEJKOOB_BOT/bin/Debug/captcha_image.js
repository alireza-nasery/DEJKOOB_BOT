var img = document.getElementsByTagName("img")[3];


function imgToBase64(img, replace) {

    // Create an empty canvas element
    var canvas = document.createElement("canvas");
    canvas.width = img.width;
    canvas.height = img.height;

    // Copy the image contents to the canvas
    var ctx = canvas.getContext("2d");
    ctx.drawImage(img, 0, 0);
    console.log(canvas)
    console.log(ctx)

    // Get the data-URL formatted image
    // Firefox supports PNG and JPEG. You could check img.src to
    // guess the original format, but be aware the using "image/jpg"
    // will re-encode the image.
    var dataURL = canvas.toDataURL("image/png");

    if (replace)
        return dataURL.replace("data:image/png;base64,", "");

    return dataURL;
}


return imgToBase64(img, true);

