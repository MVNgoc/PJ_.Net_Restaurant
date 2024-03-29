'use strict';



/**
 * PRELOAD
 * 
 * loading will be end after document is loaded
 */

const preloader = document.querySelector("[data-preaload]");

window.addEventListener("load", function () {
    preloader.classList.add("loaded");
    document.body.classList.add("loaded");
});



/**
 * add event listener on multiple elements
 */

const addEventOnElements = function (elements, eventType, callback) {
    for (let i = 0, len = elements.length; i < len; i++) {
        elements[i].addEventListener(eventType, callback);
    }
}

/**
 * USer ResetPass
 */

function validateResetPass() {
    let newpassbox = document.getElementById("pwd")
    let newpasscfbox = document.getElementById("cnpwd")
    let messageBox = document.getElementById("message")

    let newpass = newpassbox.value;
    let newpasscf = newpasscfbox.value;

    if (newpass === "") {
        messageBox.innerHTML = "Please enter your new password";
        passbox.focus();
        return false;
    } else if (newpasscf === "") {
        messageBox.innerHTML = "Please enter your new confirm password";
        newpasscfbox.focus();
        return false;
    } else if (newpass.length < 6) {
        messageBox.innerHTML = "Your password must contain at least 6 characters";
        newpassbox.focus();
        return false;
    } else if (newpass != newpasscf) {
        messageBox.innerHTML = "Your passwords do not match";
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}

/**
 * USer ChangePass
 */
function validateUserChangePass() {
    let passbox = document.getElementById("opwd")
    let newpassbox = document.getElementById("npwd")
    let newpasscfbox = document.getElementById("cnpwd")
    let messageBox = document.getElementById("message")

    let pass = passbox.value;
    let newpass = newpassbox.value;
    let newpasscf = newpasscfbox.value;

    if (pass === "") {
        messageBox.innerHTML = "Please enter your password";
        passbox.focus();
        return false;
    } else if (newpass === "") {
        messageBox.innerHTML = "Please enter your new password";
        newpassbox.focus();
        return false;
    } else if (newpasscf === "") {
        messageBox.innerHTML = "Please enter your new confirm password";
        newpasscfbox.focus();
        return false;
    } else if (newpass.length < 6) {
        messageBox.innerHTML = "Your password must contain at least 6 characters";
        newpassbox.focus();
        return false;
    } else if (newpass != newpasscf) {
        messageBox.innerHTML = "Your passwords do not match";
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}
/**
 * USer Info
 */
function validateUserInfo() {
    let namebox = document.getElementById("name")
    let addressBox = document.getElementById("address")
    let phoneBox = document.getElementById("phonenum")
    let emailBox = document.getElementById("email")
    let messageBox = document.getElementById("message")


    const validateEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    let name = namebox.value
    let address = addressBox.value
    let email = emailBox.value
    let phone = phoneBox.value

    if (name === "") {
        messageBox.innerHTML = "Please enter your name";
        nameBox.focus();
        return false;
    }
    else if (address === "") {
        messageBox.innerHTML = "Please enter your address";
        addressBox.focus();
        return false;
    }
    else if (email === "") {
        messageBox.innerHTML = "Please enter your email";
        emailBox.focus();
        return false;
    }
    else if (phone === "") {
        messageBox.innerHTML = "Please enter your phone number";
        phoneBox.focus();
        return false;
    }
    else if (validateEmail.test(email) !== true) {
        messageBox.innerHTML = "Your email is not correct";
        emailBox.focus();
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}


/**
 * REGISTER
 */
function validateRegisterInput() {
    let emailBox = document.getElementById("email")
    let passwordBox = document.getElementById("pwd")
    let passwordcfBox = document.getElementById("cpwd")
    let messageBox = document.getElementById("message")
    let nameBox = document.getElementById("name")
    let addressBox = document.getElementById("address")

    const validateEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    let email = emailBox.value;
    let password = passwordBox.value;
    let name = nameBox.value;
    let address = addressBox.value;
    let passwordcf = passwordcfBox.value;

    if (name === "") {
        messageBox.innerHTML = "Please enter your name";
        nameBox.focus();
        return false;
    }
    else if (address === "") {
        messageBox.innerHTML = "Please enter your address";
        addressBox.focus();
        return false;
    }
    else if (email === "") {
        messageBox.innerHTML = "Please enter your email";
        emailBox.focus();
        return false;
    }
    else if (validateEmail.test(email) !== true) {
        messageBox.innerHTML = "Your email is not correct";
        emailBox.focus();
        return false;
    }
    else if (password === "") {
        messageBox.innerHTML = "Please enter your password";
        passwordBox.focus();
        return false;
    }
    else if (passwordcf === "") {
        messageBox.innerHTML = "Please enter your confirm password";
        passwordcfBox.focus();
        return false;
    }
    else if (password.length < 6) {
        messageBox.innerHTML = "Your password must contain at least 6 characters";
        passwordBox.focus();
        return false;
    } else if (password != passwordcf) {
        messageBox.innerHTML = "Your passwords do not match";
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}

/**
 * LOGIN
 */

function validateInput() {
    let emailBox = document.getElementById("email")
    let passwordBox = document.getElementById("pwd")
    let messageBox = document.getElementById("message")

    const validateEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    let email = emailBox.value;
    let password = passwordBox.value;


    if (email === "") {
        messageBox.innerHTML = "Please enter your email";
        emailBox.focus();
        return false;
    }
    else if (validateEmail.test(email) !== true) {
        messageBox.innerHTML = "Your email is not correct";
        emailBox.focus();
        return false;
    }
    else if (password === "") {
        messageBox.innerHTML = "Please enter your password";
        passwordBox.focus();
        return false;
    }
    else if (password.length < 6) {
        messageBox.innerHTML = "Your password must contain at least 6 characters";
        passwordBox.focus();
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}
function clearError() {
    let messageBox = document.getElementById("message").innerHTML = "";
}

window.onload = function () {
    const eyeBtn = document.querySelector('#eye');
    eyeBtn.addEventListener('click', showEye);
}

function showEye() {

    const temp = document.getElementById('pwd')

    if (temp.type === "password") {
        temp.type = "text"
    } else {
        temp.type = "password"
    }
}

/**
 * ForgotPass
 */

function validateEmail() {
    let emailBox = document.getElementById("email")
    let messageBox = document.getElementById("message")
    const validateEmail = /^(([^<>()[\]\.,;:\s@\"]+(\.[^<>()[\]\.,;:\s@\"]+)*)|(\".+\"))@(([^<>()[\]\.,;:\s@\"]+\.)+[^<>()[\]\.,;:\s@\"]{2,})$/i;

    let email = emailBox.value;

    if (email === "") {
        messageBox.innerHTML = "Please enter your email";
        emailBox.focus();
        return false;
    }
    else if (validateEmail.test(email) !== true) {
        messageBox.innerHTML = "Your email is not correct";
        emailBox.focus();
        return false;
    }
    messageBox.innerHTML = "";
    return true;
}


/**
 * NAVBAR
 */

const navbar = document.querySelector("[data-navbar]");
const navTogglers = document.querySelectorAll("[data-nav-toggler]");
const overlay = document.querySelector("[data-overlay]");

const toggleNavbar = function () {
    navbar.classList.toggle("active");
    overlay.classList.toggle("active");
    document.body.classList.toggle("nav-active");
}

addEventOnElements(navTogglers, "click", toggleNavbar);



/**
 * HEADER & BACK TOP BTN
 */

const header = document.querySelector("[data-header]");
const backTopBtn = document.querySelector("[data-back-top-btn]");

let lastScrollPos = 0;

const hideHeader = function () {
    const isScrollBottom = lastScrollPos < window.scrollY;
    if (isScrollBottom) {
        header.classList.add("hide");
    } else {
        header.classList.remove("hide");
    }

    lastScrollPos = window.scrollY;
}

window.addEventListener("scroll", function () {
    if (window.scrollY >= 50) {
        header.classList.add("active");
        backTopBtn.classList.add("active");
        hideHeader();
    } else {
        header.classList.remove("active");
        backTopBtn.classList.remove("active");
    }
});



/**
 * HERO SLIDER
 */

const heroSlider = document.querySelector("[data-hero-slider]");
const heroSliderItems = document.querySelectorAll("[data-hero-slider-item]");
const heroSliderPrevBtn = document.querySelector("[data-prev-btn]");
const heroSliderNextBtn = document.querySelector("[data-next-btn]");

let currentSlidePos = 0;
let lastActiveSliderItem = heroSliderItems[0];
lastActiveSliderItem.classList.add("active");

const updateSliderPos = function () {
    lastActiveSliderItem.classList.remove("active");
    heroSliderItems[currentSlidePos].classList.add("active");
    lastActiveSliderItem = heroSliderItems[currentSlidePos];
}

const slideNext = function () {
    if (currentSlidePos >= heroSliderItems.length - 1) {
        currentSlidePos = 0;
    } else {
        currentSlidePos++;
    }

    updateSliderPos();
}

heroSliderNextBtn.addEventListener("click", slideNext);

const slidePrev = function () {
    if (currentSlidePos <= 0) {
        currentSlidePos = heroSliderItems.length - 1;
    } else {
        currentSlidePos--;
    }

    updateSliderPos();
}

heroSliderPrevBtn.addEventListener("click", slidePrev);

/**
 * auto slide
 */

let autoSlideInterval;

const autoSlide = function () {
    autoSlideInterval = setInterval(function () {
        slideNext();
    }, 7000);
}

addEventOnElements([heroSliderNextBtn, heroSliderPrevBtn], "mouseover", function () {
    clearInterval(autoSlideInterval);
});

addEventOnElements([heroSliderNextBtn, heroSliderPrevBtn], "mouseout", autoSlide);

window.addEventListener("load", autoSlide);



/**
 * PARALLAX EFFECT
 */

const parallaxItems = document.querySelectorAll("[data-parallax-item]");

let x, y;

window.addEventListener("mousemove", function (event) {

    x = (event.clientX / window.innerWidth * 10) - 5;
    y = (event.clientY / window.innerHeight * 10) - 5;

    // reverse the number eg. 20 -> -20, -5 -> 5
    x = x - (x * 2);
    y = y - (y * 2);

    for (let i = 0, len = parallaxItems.length; i < len; i++) {
        x = x * Number(parallaxItems[i].dataset.parallaxSpeed);
        y = y * Number(parallaxItems[i].dataset.parallaxSpeed);
        parallaxItems[i].style.transform = `translate3d(${x}px, ${y}px, 0px)`;
    }

});


/**
 * PRODUCT
 */
window.onload = function product() {
    let preveiwContainer = document.querySelector('.products-preview');
    let previewBox = preveiwContainer.querySelectorAll('.preview');

    document.querySelectorAll('.products-container .product').forEach(product => {
        product.onclick = () => {
            preveiwContainer.style.display = 'flex';
            let name = product.getAttribute('data-name');
            previewBox.forEach(preview => {
                let target = preview.getAttribute('data-target');
                if (name == target) {
                    preview.classList.add('active');
                }
            });
        };
    });

    previewBox.forEach(close => {
        close.querySelector('.fa-times').onclick = () => {
            close.classList.remove('active');
            preveiwContainer.style.display = 'none';
        };
    });
}
product()
/**
 * CART
 */