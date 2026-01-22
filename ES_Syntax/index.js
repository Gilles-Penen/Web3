// ES5 Syntax

// forEach()

const arrCourse = ["web1","web2","web3", "programeren1"];
for ( let idx = 0; idx < arrCourse.length; idx++){

    if(idx === 2){
        break;
        }
    const Element = arrCourse[idx];
    console.log(Element);
}
    arrCourse.forEach((e) => {console.log(e)});


    // MAP methode

const arrNumber = [1,2,3,4,5];
console.log("voor de map:", arrNumber);

const newArr = arrNumber.map((e) => e * 5);
console.log("Na de Map:",newArr);
console.log("Na de Map origineel:",arrNumber);

const arrString = arrNumber.map((e)  => e.toString());
console.log(arrString);

//REDUCE methode
const SumOfArr = arrNumber.reduce((acc,val)=> acc + val) 
//ALs je het in een body steekt ( {} ) is het undifiend: reden => log is iets teruggeven body geeft niet iets terug.
// acc = accumulator , val = value ( eigenlijk vorig en huidige)
console.log("De som is:",SumOfArr);


// enkel oneven elementen optellen

const sumOdd = arrNumber.reduce((acc,val) => {
    if (val % 2 !== 0){
        return acc + val
    } else{
        return acc;
    }
}, 0);
console.log(sumOdd)

//FILTER Methode
const filteredCourse = arrCourse.filter((e) => e !== "web3");
console.log(filteredCourse);

// SOME Methode
//minstens 1 element gelijk aan
const isProgrameren = arrCourse.some((e) => {
    return e === "programeren1";
})
console.log(isProgrameren);

// EVERY Methode
const isEveryProgrameren = arrCourse.every((e) => {
 return e === "programeren1";
}) 
console.log(isEveryProgrameren)
