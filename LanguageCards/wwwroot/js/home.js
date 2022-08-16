const open = document.getElementById("open");
const modal_container = document.getElementById("modal_container");


open.addEventListener('click', () => {
    modal_container.classList.add('show');
    open.classList.add('hide');
    document.getElementById('welcome').classList.add('hide');
    document.getElementById('footer').classList.add('dark');
})


const err = document.getElementById("err");
const lgn = document.getElementById("lgnbtn");


function check_cookie_name(name) {
    var match = document.cookie.match(new RegExp('(^| )' + name + '=([^;]+)'));
    if (match) {
        console.log(match[2]);
    }
    else {
        console.log('--something went wrong---');
    }
}

lgn.addEventListener('click', () => {
    
})
