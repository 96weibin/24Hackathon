//Vue config reference https://cli.vuejs.org/zh/config/
// In local dev environment: http://localhost:44325/
// Using process.env.BASE_URL in code to access actual baseUrl
const baseUrl = process.env.NODE_ENV === 'production' ? '/KnowledgeBase/' : '/';
module.exports = {
  publicPath: baseUrl,
  lintOnSave: false,
  filenameHashing: false,
  productionSourceMap:false,
  pages:{
    index:{
      filename:'Index.cshtml',
      entry: 'src/main.ts',
      template: 'public/index.html',
      title:'AI QnA',
    },
  }
}