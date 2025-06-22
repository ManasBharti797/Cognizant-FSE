import React from 'react';

const BlogDetails = ({ show }) => {
  if (!show) return null;

  return (
    <div className="section">
      <h1>Blog Details</h1>
      <h3 id="bg">React Learning</h3>
      <p id="sd">Stephen Biz</p>
      <p>Welcome to learning React!</p>
      <h3 id="bg">Installation</h3>
      <p id="sd">Schwedenizer</p>
      <p>You can install React from npm</p>
    </div>
  );
};

export default BlogDetails;
