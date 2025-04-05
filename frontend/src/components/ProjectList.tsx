import { useState, useEffect } from 'react';

function Headlines() {
  const [headlines, setHeadlines] = useState([]);

  useEffect(() => {
    fetch('https://localhost:5000/Water/ContentRecommendations?articleId=Uber%20lan%C3%A7a%20servi%C3%A7o%20de%20helic%C3%B3ptero%20em%20SP%20com%20pre%C3%A7os%20a%20partir%20de%20R%2466%20-%20IDG%20Now%21') // replace with your API URL
      .then(response => response.json())
      .then(data => {
        // data is an array of strings like your example
        setHeadlines(data);
      })
      .catch(error => {
        console.error('Error fetching headlines:', error);
      });
  }, []);

  return (
    <div>
      <h1>Headlines</h1>
      {headlines.length > 0 && (
        <div>
          <h2>Article</h2>
          <p>{headlines[0]}</p>
        </div>
      )}
      {headlines.length > 1 && (
        <div>
          <h2>Recommendations</h2>
          <ul>
            {headlines.slice(1, 6).map((headline, index) => (
              <ol key={index}>{index + 1}: {headline}</ol>
            ))}
          </ul>
        </div>
      )}
    </div>
  );
}

export default Headlines;