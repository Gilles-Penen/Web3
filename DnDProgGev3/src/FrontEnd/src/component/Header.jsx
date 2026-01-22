import React from "react";
import { Link } from "react-router-dom";

export default function Header() {
  return (
    <header>
      <nav>
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>
          <li>
            <Link to="/spells">Spells</Link>
          </li>
          <li>
            <Link to="/searchspells">Specific Spell</Link>
          </li>
          <li>
            <Link to="/monsters">Monsters</Link>
          </li>
          <li>
            <Link to="/about">About</Link>
          </li>
          
        </ul>
      </nav>
    </header>
  );
}