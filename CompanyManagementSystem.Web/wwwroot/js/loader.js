document.addEventListener("DOMContentLoaded", function () {
    document.getElementById("svg").style.opacity = 0;
    setTimeout(function () {
        document.getElementById("svg").style.display = "none";
        document.getElementById("bg").style.display = "block";
    }, 300);
});