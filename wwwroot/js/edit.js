const open = document.getElementsByClassName("open");
const modal_container = document.getElementById("modal_container");


const rus = document.getElementById('rus');
const eng = document.getElementById('eng');


const russian_words = document.getElementsByClassName("russian_word");
const english_words = document.getElementsByClassName("english_word");


for (let i = 0; i < open.length; i++) {
    open[i].addEventListener('click', () => {
        rus.value = russian_words[i].innerHTML;
        eng.value = english_words[i].innerHTML;
        modal_container.classList.add('show');
    })
}

const exit = document.getElementById("exit");


exit.addEventListener('click', () => {
    modal_container.classList.remove('show');
})