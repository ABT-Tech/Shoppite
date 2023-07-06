$(function () {
    $(".heart").on("click", function () {
        $(this).toggleClass("is-active");
    });
});
function imageViewer () {
let imgs = document.getElementsByClassName("img"),
  out  = document.getElementById("img-out");
for(let i = 0; i < imgs.length; i++) {
if(!imgs[i].classList.contains("el")){
  imgs[i].classList.add("el");
  imgs[i].addEventListener("click", lightImage);
  function lightImage(){
    let container = document.getElementsByClassName("img-panel")[i];
    container.classList.toggle("img-panel__selct");
  };
  imgs[i].addEventListener("click", openImage);
  function openImage () {
    let imgElement = document.createElement("img"),
        imgWrapper = document.createElement("div"),
        imgClose   = document.createElement("div"),
        container  = document.getElementsByClassName("img-panel")[i];
    container.classList.add("img-panel__selct");
    imgElement.setAttribute("class", "image__selected");
    imgElement.src = imgs[i].src;
    imgWrapper.setAttribute("class", "img-wrapper");
    imgClose.setAttribute("class", "img-close");
    imgWrapper.appendChild(imgElement);
    imgWrapper.appendChild(imgClose);
    setTimeout(
      function(){ 
        imgWrapper.classList.add("img-wrapper__initial");
        imgElement.classList.add("img-selected-initial");
      }, 50);
    out.appendChild(imgWrapper);
    imgClose.addEventListener("click", function(){
      container.classList.remove("img-panel__selct");
      out.removeChild(imgWrapper);
    });
  }
}
}
}
imageViewer();
let picRender = {
imgObjArr: [],
imgInput : document.getElementById("img-upload"),
imgOutput: document.getElementById("display-box"),
generateRandomId: function () {
let id = "";
for (let i = 0; i < 5; i++) {
  id += Math.floor((Math.random() * 10) + 1);
}
return id;
},
renderImages: function () {
picRender.imgInput.addEventListener("change", function(){
  for (let i = 0; i < picRender.imgInput.files.length; i++) {
    let randomId = picRender.generateRandomId(),
        imgObj   = {
          imgEl   : document.createElement("img"),
          imgPanel: document.createElement("div"),
          fileData: picRender.imgInput.files[i],
          fileName: picRender.imgInput.files[i].name,
          fileSize: picRender.imgInput.files[i].size,
          fileId  : "fileId_" + picRender.imgInput.files[i].name + randomId,
          configImage: function () {
            this.imgEl.setAttribute("class", "img");
            this.imgEl.src = URL.createObjectURL(this.fileData);
            this.imgPanel.setAttribute("id", this.fileId);
            this.imgPanel.setAttribute("class", "img-panel");
            this.imgPanel.appendChild(this.imgEl);
            picRender.imgOutput.appendChild(this.imgPanel);
            imageViewer();
          }
        };
    imgObj.configImage();
    picRender.imgObjArr.push(imgObj);
  };
});
},
}
picRender.renderImages();
class ImageViewer {
constructor(imgArr, imgContArr, imgOutput) {
this.imgArr     = imgArr;
this.imgContArr = imgContArr;
this.imgOutput   = imgOutput;
this.initialize();
}
initialize () {
for( let i = 0; i < this.imgArr.length; i++) {
  let img = this.imgArr[i];
  if(!img.classList.contains("el")) {
    img.classList.add("el");
    img.addEventListener("click", function (){         
      let imgElement = document.createElement("div"),
          imgWrapper = document.createElement("div"),
          imgClose   = document.createElement("div"),
          container = this.imgContArr[i];
      container.classList.toggle("img-panel__selct");
      imgClose.setAttribute("class", "img-close");
      imgElement.setAttribute("class", "image__selected");
      imgElement.src = imgs[i].src;
      imgWrapper.setAttribute("class","img-wrapper");
      imgWrapper.appendChild(imgElement);
      imgWrapper.appendChild(imgClose);
      setTimeout(
      function (){
        imgWrapper.classList.add("img-wrapper__initial");
        imgElement.classList.add("img-selected-initial");
      }, 50);
      this.imgOutput.appendChild(imgWrapper);
      imgclose.addeventListener("click", function(){
        container.classList.remove("img-panel__selct");
        this.imgOutput.removeChild(imgWrapper);
      })   
    });
  }
}
}
}
function detailsreadmore() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("more");
  var btnText = document.getElementById("detailsbutton");
  if (dots.style.display === "none") {
    dots.style.display = "inline";
    btnText.innerHTML = "Read more"; 
    moreText.style.display = "none";
  } else {
    dots.style.display = "none";
    btnText.innerHTML = "Read less"; 
    moreText.style.display = "inline";
  }
}
function singlereviewdetailsreadmore(id) {
  var dots = document.getElementById("dots1");
  var moreText = document.getElementById("more1");
  var btnText = document.getElementById(id);

  if (dots.style.display === "none") {
    dots.style.display = "inline";
    btnText.innerHTML = "Read more"; 
    moreText.style.display = "none";
  } else {
    dots.style.display = "none";
    btnText.innerHTML = "Read less"; 
    moreText.style.display = "inline";
  }
}

