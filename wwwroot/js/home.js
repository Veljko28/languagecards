const open = document.getElementById("open");
const modal_container = document.getElementById("modal_container");


open.addEventListener('click', () => {
    modal_container.classList.add('show');
    open.classList.add('hide');
    document.getElementById('welcome').classList.add('hide');
    document.getElementById('footer').classList.add('dark');
})