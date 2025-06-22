import React from 'react';
import BookDetails from './components/BookDetails';
import BlogDetails from './components/BlogDetails';
import CourseDetails from './components/CourseDetails';
import { books } from './data/books';
import './App.css';

function App() {
  const showBlog = true;

  return (
    <div className="app-container">
      <div className="section">
        <CourseDetails />
      </div>
      <div className="divider"></div>
      <div className="section">
        <BookDetails books={books} />
      </div>
      <div className="divider"></div>
      <div className="section">
        <BlogDetails show={showBlog} />
      </div>
    </div>
  );
}

export default App;