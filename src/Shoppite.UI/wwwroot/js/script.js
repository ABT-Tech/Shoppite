
// <!--scrollup-start-->

// Get the button
let mybutton = document.getElementById("myBtn");

// When the user scrolls down 20px from the top of the document, show the button
window.onscroll = function() {scrollFunction()};

function scrollFunction() {
  if (document.body.scrollTop > 20 || document.documentElement.scrollTop > 20) {
    mybutton.style.display = "block";
  } else {
    mybutton.style.display = "none";
  }
}

// When the user clicks on the button, scroll to the top of the document
function topFunction() {
  document.body.scrollTop = 0;
  document.documentElement.scrollTop = 0;
}

// <!--scrollup-end-->

// <!--navbarsreach-start-->
'use strict';

var searchBox = document.querySelectorAll('.search-box input[type="text"] + span');

searchBox.forEach(elm => {
  elm.addEventListener('click', () => {
    elm.previousElementSibling.value = '';
  });
});
// <!--navbarsreach-end-->

// <!--navbarbuttontooltip-start-->
$(function () {
  $('[data-toggle="tooltip"]').tooltip()
})
// <!--navbarbuttontooltip-start-->

// <!--navbardropdawon-start-->
/* When the user clicks on the button, 
  toggle between hiding and showing the dropdown content */
  function myFunction() {
    document.getElementById("myDropdown").classList.toggle("show");
  }
  // Close the dropdown if the user clicks outside of it
  window.onclick = function (event) {
    if (!event.target.matches('.dropbtn')) {
      var dropdowns = document.getElementsByClassName("dropdown-content");
      var i;
      for (i = 0; i < dropdowns.length; i++) {
        var openDropdown = dropdowns[i];
        if (openDropdown.classList.contains('show')) {
          openDropdown.classList.remove('show');
        }
      }
    }
  }
// <!--navbardropdawon-end-->

// <!--sliderbar-start-->
let slideIndex = 0;
showSlides();

function showSlides() {
  let i;
  let slides = document.getElementsByClassName("mySlides");
  let dots = document.getElementsByClassName("dot");
  for (i = 0; i < slides.length; i++) {
    slides[i].style.display = "none";  
  }
  slideIndex++;
  if (slideIndex > slides.length) {slideIndex = 1}    
  for (i = 0; i < dots.length; i++) {
    dots[i].className = dots[i].className.replace(" active", "");
  }
  slides[slideIndex-1].style.display = "block";  
  dots[slideIndex-1].className += " active";
  setTimeout(showSlides, 4000); // Change image every 2 seconds
}
// <!--sliderbar-end-->

// <!--toppicks-start-->
"use strict";

productScroll();

function productScroll() {
  let slider = document.getElementById("slider");
  let next = document.getElementsByClassName("pro-next");
  let prev = document.getElementsByClassName("pro-prev");
  let slide = document.getElementById("slide");
  let item = document.getElementById("slide");

  for (let i = 0; i < next.length; i++) {
    //refer elements by class name

    let position = 0; //slider postion

    prev[i].addEventListener("click", function() {
      //click previos button
      if (position > 0) {
        //avoid slide left beyond the first item
        position -= 1;
        translateX(position); //translate items
      }
    });

    next[i].addEventListener("click", function() {
      if (position >= 0 && position < hiddenItems()) {
        //avoid slide right beyond the last item
        position += 1;
        translateX(position); //translate items
      }
    });
  }

  function hiddenItems() {
    //get hidden items
    let items = getCount(item, false);
    let visibleItems = slider.offsetWidth / 280;
    return items - Math.ceil(visibleItems);
  }
}

function translateX(position) {
  //translate items
  slide.style.left = position * -280 + "px";
}

function getCount(parent, getChildrensChildren) {
  //count no of items
  let relevantChildren = 0;
  let children = parent.childNodes.length;
  for (let i = 0; i < children; i++) {
    if (parent.childNodes[i].nodeType != 3) {
      if (getChildrensChildren)
        relevantChildren += getCount(parent.childNodes[i], true);
      relevantChildren++;
    }
  }
  return relevantChildren;
  
}
// <!--toppicks-end-->

// <!--BudgetbuysSection-start-->
$(document).ready(function () {

  $(".owl-carousel").owlCarousel({

    autoPlay: 3000,
    items: 4,
    itemsDesktop: [1199, 3],
    itemsDesktopSmall: [979, 3],
    center: true,
    nav: true,
    loop: true,
    responsive: {
      600: {
        items: 4
      }
    }
  });

});

var myLink = document.querySelector('a[href="#"]');
    myLink.addEventListener('click', function (e) {
      e.preventDefault();
    });
// <!--BudgetbuysSection-end-->

// <!--brandslashedSection-start-->
$('.owl-carouselslashed').owlCarousel({
  autoPlay: 3000,
  loop:true,
  margin:10,
  center: true,
  nav: true,
  responsiveClass:true,
  responsive:{
      0:{
          items:3,
          nav:true
      },
      600:{
          items:4,
          nav:false
      },
      1000:{
          items:5,
          nav:true,
          loop:false
      }
  }
})
// <!--brandslashedSection-start-->

// <!--pricedropSection-start-->
$(document).ready(function() {
 
  $("#owl-demo").owlCarousel({
      items : 4, //10 items above 1000px browser width
      itemsDesktop : [1000,5], //5 items between 1000px and 901px
      itemsDesktopSmall : [900,2], // betweem 900px and 601px
      itemsTablet: [600,2], //2 items between 600 and 0;
      itemsMobile : [600,1], // itemsMobile disabled - inherit from itemsTablet option
      autoPlay : true
  });
 
});
// <!--pricedropSection-end-->

// /* <!--mainpageofferSection-start--> */
var left_side=document.querySelector(".left-side");
    var card=document.querySelector(".mainpageoffer");
    var triangle=document.querySelector(".triangle");
    
    left_side.addEventListener('click',function(){
    card.classList.toggle('widen');
    triangle.classList.toggle('righttriangle');
        
    });
// /* <!--mainpageofferSection-end--> */

// /* <!--wishlistpage-start--> */
function wishList(){
    var list = document.getElementById("toast");
  list.classList.add("show");
  list.innerHTML = '<i class="fas fa-trash-alt fa-bounce wish"></i> <span class="font-weight-bold" style="color: #000000;">Product Removed From Whishlist</span>';
  setTimeout(function(){
    list.classList.remove("show");
  },3000);
}
function addCart(){
      var cart = document.getElementById("toast-cart");
  cart.classList.add("show");
  cart.innerHTML = '<i class="fas fa-shopping-cart fa-bounce cart"></i> <span class="font-weight-bold" style="color: #000000;">Product Move To Bag</span>';
  setTimeout(function(){
    cart.classList.remove("show");
  }, 3000);
}
// /* <!--wishlistpage-end--> */


var myLink = document.querySelector('a[href="#"]');
    myLink.addEventListener('click', function (e) {
      e.preventDefault();
    });

// /* <!--productslistpage-start--> */
function brandFunction() {
    var numItems = $('.brandcount').length;
    console.log(numItems);
      var dots = document.getElementById("dots");
      var moreText = document.getElementById("brandfilter");
      var btnText = document.getElementById("brandfilterbutton");

      if (dots.style.display === "none") {
          dots.style.display = "inline";
          btnText.innerHTML = "+ (6) More";
          moreText.style.display = "none";
      } else {
          dots.style.display = "none";
          btnText.innerHTML = "Read less";
          moreText.style.display = "inline";
      }
  }



  function categoriesFunction() {
      var dots = document.getElementById("dots");
      var numofCategories = $('.categoriescount').length;
      console.log(numofCategories)
    var moreText = document.getElementById("categorieshidefilter");
    var btnText = document.getElementById("categoriesfilterbutton");

    if (dots.style.display === "none") {
        dots.style.display = "inline";
        btnText.innerHTML = "+ (1) More";
        moreText.style.display = "none";
    } else {
        dots.style.display = "none";
        btnText.innerHTML = "Read less";
        moreText.style.display = "inline";
    }
}

function sizeFunction() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("sizehidefilter");
  var btnText = document.getElementById("sizefilterbutton");

  if (dots.style.display === "none") {
      dots.style.display = "inline";
      btnText.innerHTML = "+ (3) More";
      moreText.style.display = "none";
  } else {
      dots.style.display = "none";
      btnText.innerHTML = "Read less";
      moreText.style.display = "inline";
  }
}

function colorFunction() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("colorhidefilter");
  var btnText = document.getElementById("colorfilterbutton");

  if (dots.style.display === "none") {
      dots.style.display = "inline";
      btnText.innerHTML = "+ (2) More";
      moreText.style.display = "none";
  } else {
      dots.style.display = "none";
      btnText.innerHTML = "Read less";
      moreText.style.display = "inline";
  }
}

function priceFunction() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("pricehidefilter");
  var btnText = document.getElementById("pricefilterbutton");

  if (dots.style.display === "none") {
      dots.style.display = "inline";
      btnText.innerHTML = "+ (2) More";
      moreText.style.display = "none";
  } else {
      dots.style.display = "none";
      btnText.innerHTML = "Read less";
      moreText.style.display = "inline";
  }
}

function rageFunction() {
  var dots = document.getElementById("dots");
  var moreText = document.getElementById("ragehidefilter");
  var btnText = document.getElementById("ragefilterbutton");

  if (dots.style.display === "none") {
      dots.style.display = "inline";
      btnText.innerHTML = "+ (3) More";
      moreText.style.display = "none";
  } else {
      dots.style.display = "none";
      btnText.innerHTML = "Read less";
      moreText.style.display = "inline";
  }
}   

function change_image(image){
  var image_container = document.getElementById("main-image");
image_container.src = image.src;
}

function change_imageone(image){
  var image_container = document.getElementById("main-imageone");
image_container.src = image.src;
}

function change_imagetwo(image){
  var image_container = document.getElementById("main-imagetwo");
image_container.src = image.src;
}

function change_imagethree(image){
  var image_container = document.getElementById("main-imagethree");
image_container.src = image.src;
}

function change_imagefour(image){
  var image_container = document.getElementById("main-imagefour");
image_container.src = image.src;
}
// /* <!--productslistpage-end--> */

function paymentwall() {
  document.getElementById("show1").classList.remove("hide");
  document.getElementById("show2").classList.add("hide");
}

function upipayment1() {
  document.getElementById("upi1").classList.remove("hide");
  document.getElementById("upi2").classList.add("hide");
}

function upipayment2() {
  document.getElementById("upi1").classList.add("hide");
  document.getElementById("upi2").classList.remove("hide");
}
    
