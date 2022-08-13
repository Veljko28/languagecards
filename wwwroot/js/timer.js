const getTimePassed = () => {
    const timer = document.getElementById("timer");
    let a = timer.innerHTML;
    let m = parseInt(a.substring(0, 2)), s = parseInt(a.substring(3, 5));
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