



function UI()
{
    this.topbar_buttons=document.querySelectorAll("#buttons a");
    this.homeCards=document.querySelectorAll("#itemCards #card");
    this.homeCards_like=document.querySelectorAll("#itemCards #card #sembol");
}


document.addEventListener("DOMContentLoaded",function(){
    const ui=new UI();

    for(let item of ui.topbar_buttons)
    {
        item.addEventListener("mouseenter",topbarEnter);
        item.addEventListener("mouseleave",topbarLeave);
    }
    for(let item of ui.homeCards)
    {
        item.addEventListener("mouseenter",cardEnter);
        item.addEventListener("mouseleave",cardLeave);
    }
    for(let item of ui.homeCards_like)
    {
        item.addEventListener("mouseenter",cardlikeEnter);
        item.addEventListener("mouseleave",cardlikeLeave);
    }
});

function topbarEnter(e)
{
    e.preventDefault();
    const element=e.target;
    element.classList.add("f1");
    element.classList.add("btn-warning");
}
function topbarLeave(e)
{
    e.preventDefault();
    const element=e.target;
    element.classList.remove("f1");
    element.classList.remove("btn-warning");
}




function cardEnter(e)
{
    e.preventDefault();
    const element=e.target;
    const lastelement=e.target.children[3];
    element.classList.add("f2");
    lastelement.classList.remove("d-none");
}
function cardLeave(e)
{
    e.preventDefault();
    const element=e.target;
    const lastelement=e.target.children[3];
    element.classList.remove("f2");
    lastelement.classList.add("d-none");

}



function cardlikeEnter(e)
{
    e.preventDefault();
    const element=e.target;
    element.classList.add("f3");
}
function cardlikeLeave(e)
{
    e.preventDefault();
    const element=e.target;
    element.classList.remove("f3");
}