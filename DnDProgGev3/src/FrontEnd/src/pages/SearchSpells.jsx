import React, { useState } from "react";
import { getSpecificSpell } from "../services/api"; // Zorg ervoor dat dit correct is geïmporteerd

export default function SearchSpell() {
  const [spellName, setSpellName] = useState(""); // Bewaar de spell naam
  const [spell, setSpell] = useState(null); // Bewaar de opgehaalde spell data
  const [loading, setLoading] = useState(false); // Laad status voor fetching

  const handleSearch = async () => {
    setLoading(true);
    const spellData = await getSpecificSpell(spellName);
    setSpell(spellData);
    setLoading(false);
  };

  return (
    <div className="search-spell">
      <h1>Search for a Spell</h1>

      {/* Voeg de class 'search-bar' toe aan de container div */}
      <div className="search-bar">
        <input
          type="text"
          placeholder="Enter spell name"
          value={spellName}
          onChange={(e) => setSpellName(e.target.value)}
        />
        
        {/* Voeg de class 'button' toe aan de zoekknop */}
        <button onClick={handleSearch}>Search</button>
      </div>

      {loading && <p>Loading...</p>}

      {spell && (
        <div className="spell-info">
          <h2>{spell.name}</h2>
          <p><strong>Description:</strong></p>
          <ul>
            {spell.desc.map((description, index) => (
              <li key={index}>{description}</li>
            ))}
          </ul>

          <p><strong>Range:</strong> {spell.range}</p>
          <p><strong>Components:</strong> {spell.components.join(", ")}</p>
          <p><strong>Material:</strong> {spell.material}</p>
          <p><strong>Ritual:</strong> {spell.ritual ? "Yes" : "No"}</p>
          <p><strong>Duration:</strong> {spell.duration}</p>
          <p><strong>Concentration:</strong> {spell.concentration ? "Yes" : "No"}</p>
          <p><strong>Casting Time:</strong> {spell.casting_time}</p>
          <p><strong>Level:</strong> {spell.level}</p>
          
          <h3>Damage Information</h3>
          <p><strong>Damage Type:</strong> {spell.damage?.damage_type?.name}</p>
          <p><strong>Damage at Slot Level:</strong></p>
          <ul>
            {Object.entries(spell.damage?.damage_at_slot_level).map(([level, damage], index) => (
              <li key={index}>Level {level}: {damage}</li>
            ))}
          </ul>
          
          <p><strong>School:</strong> {spell.school?.name}</p>
          <p><strong>Classes:</strong> {spell.classes.map(cls => cls.name).join(", ")}</p>
          <p><strong>Subclasses:</strong> {spell.subclasses.map(sub => sub.name).join(", ")}</p>
        </div>
      )}
    </div>
  );
}