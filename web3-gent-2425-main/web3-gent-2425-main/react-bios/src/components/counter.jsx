import React, { useState } from "react";

const counter = () =>{

//let counter = 0;

const [counter, setCounter] = useState(0);
const[history, setHistory] = useState([]);

return <div>


    <p>{counter}</p>
    <button onClick={()=>{
        //counter--;
        setCounter(counter - 1);
    }}>-</button>
    <button onClick={()=>{
        //counter++;
        setCounter(counter + 1);
    }}>+</button>
    <button onClick={()=>{
        setHistory([...history,counter])
    }}>copy</button>
<ul>
    {history.map ((h,index) => (
        <li key={idx}>(h)</li>
        ))}
</ul>
</div>


};

export default button;