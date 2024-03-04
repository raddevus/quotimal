// get all the images on the pixabay page:
var allPics = document.querySelectorAll(".link--WHWzm");

// create the json objects for each image:
allPics.forEach(x => console.log(`{"link":"${x.href}","img":"${x.childNodes[0].src}"}`));

