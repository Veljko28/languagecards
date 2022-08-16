const getTimePassed = () => {
    const timer = document.getElementById("timer");
    let a = timer.innerHTML;
    let m = parseInt(a.substring(0, 2)), s = parseInt(a.substring(3, 5));
    const hearths = document.getElementById("hearths");
    if (hearths.innerHTML == "0") {
        alert("You lost all your hearths!");
        location.href = "/russian";
        clearInterval(startInterval);
    }
    if (s == 59) {
        if (m == 14) {
            alert("This lesson took too long. It will be finished !");
            location.href = "/russian";
            clearInterval(startInterval);
        }
        else m++,s = 0;
    }
    else s++;

    let st = s < 10 ? "0" + s.toString() : s.toString(), mt = m < 10 ? "0" + m.toString() : m.toString();
    timer.innerHTML = mt + ":" + st;
}


var startInterval = setInterval(() => getTimePassed(), 1000);
const sbm = document.getElementById("sbm");

sbm.addEventListener("click", () => {
    const alr = document.getElementById("alert-type");
    const suc = document.getElementById("sucs"), fld = document.getElementById("fld");
    console.log("testing")
    switch (alr.value) {
        case "0":
            break;
        case "1":
            suc.classList.add("show");
            fld.classList.remove("show")
            break;
        case "2":
            fld.classList.add("show");
            suc.classList.remove("show")
            break;
        default: break;
    }
})