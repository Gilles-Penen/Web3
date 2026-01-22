import { useEffect, useState } from "react";
import { getAllMonsters } from "../services/api";
import MonsterCard from "../component/MonsterCard";
import "../style.css";
import React from "react";

export default function App() {
  const [monsters, setMonsters] = useState([]);

  useEffect(() => {
    const savedMonsters = localStorage.getItem("monsters");
    if (savedMonsters) setMonsters(JSON.parse(savedMonsters));
    getAllMonsters().then((monsters) => {
      setMonsters(monsters);
      localStorage.setItem("monsters", JSON.stringify(monsters));
    });
  }, []);

  return (
    <div className="App">
      {monsters.length === 0 && <span className="loading">Loading...</span>}
      <ul className="monster-list">
        {monsters.map((monsters) => (
          <MonsterCard key={monsters.index} monster={monsters} />
        ))}
      </ul>
    </div>
  );
}
