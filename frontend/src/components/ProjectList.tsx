import { useState, useEffect } from 'react';
import { fetchRecommendation } from '../api/ProjectsAPI';
import { Recomendation } from '../types/Recomendation';

function ProjectList({ selectedCategories }: { selectedCategories: string[] }) {
  const [recommendation, setRecommendation] = useState<Recomendation | null>(null);

  useEffect(() => {
    const loadRecommendation = async () => {
      try {
        // Use the first selected category as the articleId (adjust as needed)
        const articleId = selectedCategories[0] || '';
        if (!articleId) {
          return;
        }
        const data = await fetchRecommendation(articleId);
        // Cast data to Recomendation to satisfy TypeScript
        setRecommendation(data);
      } catch (err) {
        console.error('Error fetching recommendation:', err);
      }
    };

    loadRecommendation();
  }, [selectedCategories]);

  // Check if recommendation is null before accessing its properties
  if (!recommendation) {
    return <div>No recommendations available.</div>;
  }

  return (
    <div>
      <h2>Recommendations for {recommendation.articleId}</h2>
      <ul>
        <li>{recommendation.recommendaiton1}</li>
        <li>{recommendation.recommendaiton2}</li>
        <li>{recommendation.recommendaiton3}</li>
        <li>{recommendation.recommendaiton4}</li>
        <li>{recommendation.recommendaiton5}</li>
      </ul>
    </div>
  );
}

export default ProjectList;
