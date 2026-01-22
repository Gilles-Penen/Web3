const BASE_URL = "http://localhost:3000/api";

export async function getAllSpells() {
  try {
    // Haal de spell-indexen op
    const spellIndexes = await fetch(`${BASE_URL}/spells`).then((response) =>
      response.ok ? response.json() : Promise.reject(response)
    );

    // Voeg een vertraging toe tussen verzoeken
    const delay = ms => new Promise(resolve => setTimeout(resolve, ms));

    const spells = [];

    // Loop door alle spell-indexen en haal de details op
    for (const index of spellIndexes.results) {
      const url = index.url.startsWith("/api") ? index.url.slice(4) : index.url; // Verwijder "/api" als het aan het begin staat

      try {
        const response = await fetch(BASE_URL + url);
        if (response.ok) {
          spells.push(await response.json());
        } else {
          console.error("Error fetching spell:", index.url);
        }
      } catch (err) {
        console.error("Error fetching spell:", index.url, err);
      }

      // Voeg een vertraging van 20 ms toe voor elke spell fetch
      await delay(50);
    }

    return spells;
  } catch (error) {
    console.error("Error fetching spells list:", error);
    return []; // Geef een lege array terug als de hoofdrequest mislukt
  }
}

export async function getAllMonsters() {
  try {
    const monsterIndexes = await fetch(`${BASE_URL}/monsters`).then((response) =>
      response.ok ? response.json() : Promise.reject(response)
    );

    // Voeg een vertraging toe tussen verzoeken WHY RATELIMITER WHYYYYY
    const delay = ms => new Promise(resolve => setTimeout(resolve, ms));

    const monsters = [];
    for (const index of monsterIndexes.results) {
      const url = index.url.startsWith("/api") ? index.url.slice(4) : index.url; // Verwijder "/api" als het aan het begin staat
      try {
        const response = await fetch(BASE_URL + url);
        if (response.ok) {
          monsters.push(await response.json());
        } else {
          console.error("Error fetching monster:", index.url);
        }
      } catch (err) {
        console.error("Error fetching monster:", index.url, err);
      }

      await delay(50); 
    }
    
    return monsters;
  } catch (error) {
    console.error("Error fetching monsters list:", error);
    return []; 
  }
}


export async function getSpecificSpell(spellName) {
  try {
    const response = await fetch(`${BASE_URL}/spells/${spellName}`);
    if (response.ok) {
      return await response.json();
    } else {
      throw new Error("Spell not found");
    }
  } catch (error) {
    console.error("Error fetching spell from backend:", error);
    return null;
  }
}