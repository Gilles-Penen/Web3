import CourseItem from "./CourseItem";


//CSS module -> importeer je eigenlijk JS object
import styles from "./Cources.Module.css";




<cources className="css"></cources>

const Courses = ({ courses }) => {

  const handleclick= (event) => {
    console.log(event.target);
    console.log('geklikt op mij');
   }
  return (
    <>
    <h3>className={styles.title}</h3>
    <ul>
      {courses.map((c) => (
        <CourseItem key={c} course={c} />
      ))}
    </ul>

    <button
      onclick={() => {
        {handleclick}
      }}>
      klik mij</button>

    </>
    

  );
};


// als code in rood is en foutmelding geeft altijd <> en </> 


export default Courses;
