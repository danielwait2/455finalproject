import { useState, useEffect } from 'react';

function Headlines() {
  // State for articles (populated from the GetArticlesNames endpoint)
  const [articles, setArticles] = useState([]);
  // Selected article from the dropdown; default is empty so user must select one.
  const [selectedArticle, setSelectedArticle] = useState('');
  // States for recommendations
  const [contextRecs, setContextRecs] = useState([]);
  const [contentRecs, setContentRecs] = useState([]);

  // Fetch list of articles on component mount
  useEffect(() => {
    fetch('https://localhost:5000/Water/GetArticlesNames')
      .then((response) => response.json())
      .then((data) => {
        setArticles(data);
        // Do not auto-set a default so that the placeholder remains until the user selects one.
      })
      .catch((error) => {
        console.error('Error fetching articles:', error);
      });
  }, []);

  // Fetch context recommendations when selectedArticle changes
  useEffect(() => {
    if (!selectedArticle) return;
    fetch(
      `https://localhost:5000/Water/ContextRecommendations?articleId=${encodeURIComponent(selectedArticle)}`
    )
      .then((response) => response.json())
      .then((data) => {
        setContextRecs(data);
      })
      .catch((error) => {
        console.error('Error fetching context recommendations:', error);
      });
  }, [selectedArticle]);

  // Fetch content recommendations when selectedArticle changes
  useEffect(() => {
    if (!selectedArticle) return;
    fetch(
      `https://localhost:5000/Water/ContentRecommendations?articleId=${encodeURIComponent(selectedArticle)}`
    )
      .then((response) => response.json())
      .then((data) => {
        setContentRecs(data);
      })
      .catch((error) => {
        console.error('Error fetching content recommendations:', error);
      });
  }, [selectedArticle]);

  // Handle dropdown change
  const handleArticleChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    setSelectedArticle(e.target.value);
  };

  return (
    <>
      {/* Dropdown with placeholder */}
      <div className="mb-4">
        <select
          id="article-select"
          className="w-full border p-2 rounded"
          value={selectedArticle}
          onChange={handleArticleChange}
        >
          <option value="">-- Select an Article --</option>
          {articles.map((article, index) => (
            <option key={index} value={article}>
              {article}
            </option>
          ))}
        </select>
      </div>

      {/* Flex container to display recommendations side by side */}
      <div style={{ display: 'flex', gap: '20px' }}>
        <div style={{ flex: 1 }}>
          <h2>Context Recommendation</h2>
          {contextRecs.length > 0 && (
            <>
              {contextRecs.length > 1 && (
                <div>
                  <ul style={{ listStyleType: 'none', paddingLeft: 0 }}>
                    {contextRecs.slice(1, 6).map((rec, index) => (
                      <li key={index}>
                        {index + 1}: {rec}
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </>
          )}
        </div>

        <div style={{ flex: 1 }}>
          <h2>Content Recommendation</h2>
          {contentRecs.length > 0 && (
            <>
              {contentRecs.length > 1 && (
                <div>
                  <ul style={{ listStyleType: 'none', paddingLeft: 0 }}>
                    {contentRecs.slice(1, 6).map((rec, index) => (
                      <li key={index}>
                        {index + 1}: {rec}
                      </li>
                    ))}
                  </ul>
                </div>
              )}
            </>
          )}
        </div>
      </div>
    </>
  );
}

export default Headlines;
