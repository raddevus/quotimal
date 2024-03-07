var finalQuotes = [...document.querySelectorAll(".quoteText")];

let output = "";
finalQuotes.map(el => {
    output += el.querySelector(".authorOrTitle").textContent.split(" ")[4]+" "+ el.querySelector(".authorOrTitle").textContent.split(" ")[5].trim("\n")+"|" + el.textContent.split("\n")[1].trim(" ")+"\n";
});

console.log(output);
