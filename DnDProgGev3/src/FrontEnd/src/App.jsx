import React from "react";
import { BrowserRouter as Router, Route, Routes } from "react-router-dom";
import { useState, useEffect } from "react";
import Header from "./component/Header";  
import Home from "./pages/Home";      
import Spells from "./pages/Spells";  
import SearchSpells from "./pages/SearchSpells";
import Monsters from "./pages/monsters";
import About from "./pages/About";    
import "./style.css";         

export default function App() {
  return (
    <Router>
      <Header />
      <div className="App">
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/spells" element={<Spells />} />
           <Route path="/searchspells" element={<SearchSpells />} />
          <Route path="/monsters" element={<Monsters />} />
          <Route path="/about" element={<About />} />
        </Routes>
      </div>
    </Router>
  );
}
