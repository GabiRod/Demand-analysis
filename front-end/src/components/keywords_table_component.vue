<template>
  <div id="keywords_table_component" class="keywords_table_component">
    <input
      class="search_input"
      type="text"
      name="search"
      v-model="search"
      placeholder="search"
    />
    <div class="client_table">
      <tbody>
        <div class="row" :key="post" v-for="post in filteredList">
          <div class="keyword_row">{{ post.keyword }}</div>
          <div class="keyword_row">{{ post.volume }}</div>
          <div class="keyword_row">{{ post.position }}</div>
          <div class="keyword_row">{{ post.cta }}</div>
          <div class="keyword_row">{{ post.category }}</div>
          <div class="keyword_row">{{ post.subcategory1 }}</div>
          <div class="keyword_row">{{ post.subcategory2 }}</div>
          <div class="keyword_row">{{ post.intent }}</div>
        </div>
      </tbody>
    </div>
  </div>
</template>

<script>
class Post {
  constructor(
    keyword,
    volume,
    position,
    cta,
    category,
    subcategory1,
    subcategory2,
    intent
  ) {
    this.keyword = keyword;
    this.volume = volume;
    this.position = position;
    this.cta = cta;
    this.category = category;
    this.subcategory1 = subcategory1;
    this.subcategory2 = subcategory2;
    this.intent = intent;
  }
}

export default {
  name: "keywordsTableComponent",

  data() {
    return {
      search: "",
      postList: [
        new Post(
          "Vue.js",
          "https://vuejs.org/",
          "Chris",
          "https://vuejs.org//images/logo.png"
        ),
        new Post(
          "React.js",
          "https://facebook.github.io/react/",
          "Tim",
          "https://daynin.github.io/clojurescript-presentation/img/react-logo.png"
        ),
        new Post(
          "Angular.js",
          "https://angularjs.org/",
          "Sam",
          "https://angularjs.org/img/ng-logo.png"
        ),
        new Post(
          "Ember.js",
          "http://emberjs.com/",
          "Rachel",
          "http://www.gravatar.com/avatar/0cf15665a9146ba852bf042b0652780a?s=200"
        ),
        new Post(
          "Meteor.js",
          "https://www.meteor.com/",
          "Chris",
          "http://hacktivist.in/introduction-to-nodejs-mongodb-meteor/img/meteor.png"
        ),
        new Post(
          "Aurelia",
          "http://aurelia.io/",
          "Tim",
          "https://cdn.auth0.com/blog/aurelia-logo.png"
        ),
        new Post(
          "Node.js",
          "https://nodejs.org/en/",
          "A. A. Ron",
          "https://code-maven.com/img/node.png"
        ),
        new Post(
          "Pusher",
          "https://pusher.com/",
          "Alex",
          "https://avatars1.githubusercontent.com/u/739550?v=3&s=400"
        ),
        new Post(
          "Feathers.js",
          "http://feathersjs.com/",
          "Chuck",
          "https://cdn.worldvectorlogo.com/logos/feathersjs.svg"
        ),
      ],
    };
  },

  computed: {
    filteredList() {
      return this.postList.filter((post) => {
        return post.keyword.toLowerCase().includes(this.search.toLowerCase());
      });
    },
  },
};
</script>

<style lang="scss">
.client_table_component {
  background-color: $white;
  text-align: start;
}

.clients_column_menu {
  padding: 5px 10px;
  color: grey;
  font-size: 15px;
}

.row {
  grid-template-columns: auto auto auto auto auto auto auto auto auto;
}

.clients_row {
  border-bottom: 1px solid $grey;
}

.clients_row_data {
  color: $blue;
  font-size: 12px;
  padding: 5px 5%;
}

.search_input {
  float: right;
  padding: 10px;
  border: none;
  box-shadow: 3px 3px 7px rgb(161, 161, 161);
}
.client_table {
  padding-top: 40px;
}

.client_button {
  padding: 2px 10px;
  float: right;
  text-decoration: none;
  border: 2px solid $green;
  color: $green;
  background-color: $white;
  font-size: 10px;
  margin: 2px;

  &:hover {
    background-color: $green;
    color: $white;
  }
}

.arrow_down {
  background-image: url("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB8AAAAaCAYAAABPY4eKAAAAAXNSR0IArs4c6QAAAvlJREFUSA29Vk1PGlEUHQaiiewslpUJiyYs2yb9AyRuJGm7c0VJoFXSX9A0sSZN04ULF12YEBQDhMCuSZOm1FhTiLY2Rky0QPlQBLRUsICoIN/0PCsGyox26NC3eTNn3r3n3TvnvvsE1PkwGo3yUqkkEQqFgw2Mz7lWqwng7ztN06mxsTEv8U0Aam5u7r5EInkplUol/f391wAJCc7nEAgE9Uwmkzo4OPiJMa1Wq6cFs7Ozt0H6RqlUDmJXfPIx+qrX69Ti4mIyHA5r6Wq1egND+j+IyW6QAUoul18XiUTDNHaSyGazKcZtdgk8wqhUKh9o/OMvsVgsfHJy0iWqVrcQNRUMBnd6enqc9MjISAmRP3e73T9al3XnbWNjIw2+KY1Gc3imsNHR0YV4PP5+d3e32h3K316TySQFoX2WyWR2glzIO5fLTSD6IElLNwbqnFpbWyO/96lCoai0cZjN5kfYQAYi5H34fL6cxWIZbya9iJyAhULBHAqFVlMpfsV/fHxMeb3er+Vy+VUzeduzwWC45XA4dlD/vEXvdDrj8DvURsYEWK3WF4FA4JQP9mg0WrHZbEYmnpa0NxYgPVObm5teiLABdTQT8a6vrwdRWhOcHMzMzCiXlpb2/yV6qDttMpkeshEzRk4Wo/bfoe4X9vb2amzGl+HoXNT29vZqsVi0sK1jJScG+Xx+HGkL4Tew2TPi5zUdQQt9otPpuBk3e0TaHmMDh1zS7/f780S0zX6Yni+NnBj09fUZUfvudDrNZN+GkQbl8Xi8RLRtHzsB9Hr9nfn5+SjSeWUCXC7XPq5kw53wsNogjZNohYXL2EljstvtrAL70/mVaW8Y4OidRO1/gwgbUMvcqGmcDc9aPvD1gnTeQ+0nmaInokRj0nHh+uvIiVOtVvt2a2vLv7Ky0tL3cRTXIcpPAwMDpq6R4/JXE4vFQ5FI5CN+QTaRSFCYc8vLy1l0rge4ARe5kJ/d27kYkLXoy2Jo4C7K8CZOsEBvb+9rlUp1xNXPL7v3IDwxvPD6AAAAAElFTkSuQmCC");
}
.arrow_up {
  background-image: url("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAB4AAAAaCAYAAACgoey0AAAAAXNSR0IArs4c6QAAAwpJREFUSA21Vt1PUmEYP4dvkQ8JFMwtBRocWAkDbiqXrUWXzU1rrTt0bdVqXbb1tbW16C9IBUSmm27cODdneoXjputa6069qwuW6IIBIdLvdaF4OAcOiGeDc87zPs/vd57P96WpFq7p6enbGo1mjKZpeTabjU1MTCRagGnOZHFxcXxtbe1XKpUq7+zslJeXl//Mz8+Hy+Uy3RxSE9qTk5M3otFooVQqgef4Wl9f343FYoEmoISrxuNxFX5f9vb2jhn/PxUKhfLS0tIPfFifUESRUMV8Pv/M6XReRm5rTGQyGeXxeGxYe1ezeBpBOBx2rKysbO7v79d4Wy3Y2Nj4GQqFbgnhaugxwiuGJx99Pp9FLBbXxYTXvTqd7v3MzIy6riIWGxJnMpl7AwMD14xGYyMsSq1WUyQdUqn0eSPlusQIsbGrq+vl4OCgvhFQZd1utyv1en0gEolcqsi47nWJlUrlG5fLZVcoFFy2nDKSDpIWlUoVTCQSEk4lCHmJMZ2GTCbTiMVikfIZ88l7enoos9l8dXt7+z6fDicxSJUokqDX6xXcl2wCROoc0vQCWL3sNfLOSdzR0fHY4XC4tVotl40gmVwup9xuN4OQv+UyqCFGH9rg7SOGYVRcBs3IEG4J0nVnamrqOtvuBDGGgQg9+wHFcVEi4a0LNkbdd6TrPKo8ODc311mteIIYjT/a398/jK+s1jnVM0kXoufCFvq0GuiIGEVgQIhfoygM1QrteEa9dAL7ITiYCt4RMabOK5AyKKzKWtvupLcRciu8D5J0EuDDPyT/Snd39yh6VtY2NhYQSR9G79Ds7OxdskRjEyAufvb7/cPoO5Z6e1+xtVKrq6vfcFzyi/A3ZrPZ3GdNSlwgo5ekE4X2RIQGf2C1WlufFE0GBeGWYQ8YERWLxQtnUVB830MKLZfL9RHir8lkssCn2G751tZWEWe03zTKm15YWPiEiXXTYDB0Ig/t7yd8PRws4EicwWHxO4jHD8/C5HiTTqd1BwcHFozKU89origB+y/kmzgYpgOBQP4fGmUiZmJ+WNgAAAAASUVORK5CYII=");
}
.arrow {
  float: right;
  width: 12px;
  height: 15px;
  background-repeat: no-repeat;
  background-size: contain;
  background-position-y: bottom;
}
</style>
