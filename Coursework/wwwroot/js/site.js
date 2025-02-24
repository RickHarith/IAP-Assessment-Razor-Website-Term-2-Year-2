// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const hamburger = document.querySelector(".hamburger");
const navbarContents = document.querySelector(".navbar-contents");

hamburger.addEventListener("click", () => {
    hamburger.classList.toggle("active");
    navbarContents.classList.toggle("active");
})

document.querySelectorAll(".nav-link").forEach(n => n.addEventListener("click", () => {
    hamburger.classList.remove("active");
    navbarContents.classList.remove("active");
}))

document.querySelectorAll('a[href^="#"]').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        e.preventDefault();
        document.querySelector(this.getAttribute('href')).scrollIntoView({
            behavior: 'smooth'
        });
    });
});

function increaseFontSize() {
    var elements = document.querySelectorAll("main *");
    for (var i = 0; i < elements.length; i++) {
        var currentSize = window.getComputedStyle(elements[i]).getPropertyValue("font-size");
        var newSize = parseFloat(currentSize) + 1 + "px";
        elements[i].style.fontSize = newSize;
    }
    localStorage.setItem("fontSize", newSize);
}

function decreaseFontSize() {
    var elements = document.querySelectorAll("main *");
    for (var i = 0; i < elements.length; i++) {
        var currentSize = window.getComputedStyle(elements[i]).getPropertyValue("font-size");
        var newSize = parseFloat(currentSize) - 1 + "px";
        elements[i].style.fontSize = newSize;
    }
    localStorage.setItem("fontSize", newSize);
}

function resetFontSize() {
    var elements = document.querySelectorAll("main *");
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.fontSize = ""; //removes the inline font-size style
    }
    localStorage.removeItem("fontSize");
}

var storedSize = localStorage.getItem("fontSize");
if (storedSize) {
    var elements = document.querySelectorAll("main *");
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.fontSize = storedSize;
    }
}

var increaseButton = document.getElementById("increase-font-size");
increaseButton.addEventListener("click", increaseFontSize);

var decreaseButton = document.getElementById("decrease-font-size");
decreaseButton.addEventListener("click", decreaseFontSize);

var storedSize = localStorage.getItem("fontSize");
if (storedSize) {
    var elements = document.querySelectorAll("main *");
    for (var i = 0; i < elements.length; i++) {
        elements[i].style.fontSize = storedSize;
    }
} else {
    resetFontSize();
}

const redButton = document.querySelector('#red');
const blueButton = document.querySelector('#blue');
const greenButton = document.querySelector('#green');
const yellowButton = document.querySelector('#yellow');
const whiteButton = document.querySelector('#white');

redButton.addEventListener('click', () => {
    document.documentElement.style.setProperty('--background-color', '#FF999B')
    document.documentElement.style.setProperty('--text-color', '#3B0001')
    localStorage.setItem('background-color', '#FF999B');
    localStorage.setItem('text-color', '#3B0001');
});

blueButton.addEventListener('click', () => {
    document.documentElement.style.setProperty('--background-color', '#96E1FF')
    document.documentElement.style.setProperty('--text-color', '#001061')
    localStorage.setItem('background-color', '#96E1FF');
    localStorage.setItem('text-color', '#001061');
});

greenButton.addEventListener('click', () => {
    document.documentElement.style.setProperty('--background-color', '#93EDA1')
    document.documentElement.style.setProperty('--text-color', '#004008')
    localStorage.setItem('background-color', '#93EDA1');
    localStorage.setItem('text-color', '#004008');
});

yellowButton.addEventListener('click', () => {
    document.documentElement.style.setProperty('--background-color', '#FAFA9D')
    document.documentElement.style.setProperty('--text-color', '#BF9D04')
    localStorage.setItem('background-color', '#FAFA9D');
    localStorage.setItem('text-color', '#BF9D04');
});

whiteButton.addEventListener('click', () => {
    document.documentElement.style.setProperty('--background-color', '#FFFFFF')
    document.documentElement.style.setProperty('--text-color', '#000000')
    localStorage.setItem('background-color', '#FFFFFF');
    localStorage.setItem('text-color', '#000000');
});

window.addEventListener('load', () => {
    let bgColor = localStorage.getItem('background-color');
    let textColor = localStorage.getItem('text-color');
    if (bgColor) {
        document.documentElement.style.setProperty('--background-color', bgColor);
    }
    if (textColor) {
        document.documentElement.style.setProperty('--text-color', textColor);
    }
});


const productContainer = [...document.querySelectorAll('.product-container')];
const RButton = [...document.querySelectorAll('.r-button')];
const LButton = [...document.querySelectorAll('.l-button')];

productContainer.forEach((item, i) => {
    let containerDimensions = item.getBoundingClientRect();
    let containerWidth = containerDimensions.width;

    RButton[i].addEventListener('click', () => {
        item.scrollLeft += containerWidth;
    })

    LButton[i].addEventListener('click', () => {
        item.scrollLeft -= containerWidth;
    })
})

function ValidateEmail() {
    var email = document.getElementById("EMAIL").value;

    var regx = /^([a-zA-Z0-9\._]+)@([a-zA-Z0-9])+\.([a-zA-Z]{2,})(\.[a-zA-Z]{2,})?$/

    if (email.trim() === "") {
        alert("E-mail field cannot be empty");
        return false;
    }

    else if (regx.test(email)) {
        alert("E-mail format accepted")
        return true
    }

    else {
        alert("Invalid e-mail format")
        return false;
    }
}

function CheckName() {
    var name = document.getElementById("NAME").value;

    var regx = /^[a-zA-Z\s]*$/

    if (name.trim() === "") {
        alert("Name field cannot be empty");
        return false;
    }

    else if (regx.test(name)) {
        alert("Name format accepted")
        return true
    }
    else {
        alert("Invalid name, only spaces and letters allowed")
        return false;
    }
}

