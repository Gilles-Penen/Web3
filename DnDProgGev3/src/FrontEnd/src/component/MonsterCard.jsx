import React from "react";

export default function MonsterCard({ monster }) {
    return (
      <li className="monster-card">
        <hgroup>
          <h4>{monster.name}</h4>
          <small>{monster.size} {monster.type} ({monster.alignment})</small>
        </hgroup>
        <div className="stats">
          <p>
            <strong>Armor Class</strong>
            {monster.armor_class ? monster.armor_class[0]?.value : "N/A"}
          </p>
          <p>
            <strong>Hit Points</strong>
            {monster.hit_points} HP
          </p>
          <p>
            <strong>Speed</strong>
            Walk: {monster.speed.walk}, Swim: {monster.speed.swim}
          </p>
          <p>
            <strong>Challenge Rating</strong>
            {monster.challenge_rating}
          </p>
          <div className="abilities">
            <strong>Special Abilities:</strong>
            <ul>
              {monster.special_abilities.map((ability, index) => (
                <li key={index}>
                  <strong>{ability.name}</strong>: {ability.desc}
                </li>
              ))}
            </ul>
          </div>
        </div>
      </li>
    );
  }